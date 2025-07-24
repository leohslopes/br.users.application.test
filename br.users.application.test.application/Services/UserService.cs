using br.users.application.test.domain.Entities.Messasing;
using br.users.application.test.domain.Entities.UserCx;
using br.users.application.test.domain.Interfaces.Messaging;
using br.users.application.test.domain.Interfaces.Repositories;
using br.users.application.test.domain.Interfaces.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Text;


namespace br.users.application.test.application.Services
{
    public class UserService: BaseService, IUserService
    {
        private readonly ILogger<UserService> _logger;
        private readonly IUserCxRepository _userRepository;
        private readonly IMessageBusService _messageBusService;

        public UserService(ILogger<UserService> logger, IUserCxRepository userRepository, IMessageBusService messageBusService)
        {
            _logger = logger;
            _userRepository = userRepository;
            _messageBusService = messageBusService;
        }

       public async Task<IEnumerable<Users>> GetItemsUserList(string filterName, string filterEmail, bool? filterImg)
        {
            IEnumerable<Users> result;

            try
            {
                if (!string.IsNullOrEmpty(filterName) || !string.IsNullOrEmpty(filterEmail) || filterImg.HasValue)
                {
                    result = await _userRepository.GetUsersWithFilters(filterName, filterEmail, filterImg);
                }
                else
                {
                    result = await _userRepository.GetAllUsers();
                }   
            }
            catch (ApplicationException ex)
            {
                _logger.LogError($"[GetItemsUserList] - Erro ao consultar os usuários no banco de dados: {ex.Message}");
                throw ex;
            }

            return result;
        }

        public async Task<bool> SaveNewResgisterUserData(string nameUser, string emailUser, int ageUser, string genderUser, string passwordUser, string officialNumberUser)
        {
            bool controlProcess = false;

            try
            {
                var listUsers = await _userRepository.GetAllUsers();

                if (listUsers == null || listUsers.Count() <= 0)
                {
                    _logger.LogInformation($"Tabela USERS_CX sem registros.");
                    return controlProcess;
                } 
                else
                {
                    if (listUsers.Any(x => x.UserEmail.ToUpper().Trim().Equals(emailUser.ToUpper().Trim())))
                    { 
                      _logger.LogInformation($"Já existe o e-mail {emailUser.ToUpper().Trim()} cadastrado.");
                      throw new ApplicationException($"Já existe o e-mail {emailUser.ToUpper().Trim()} cadastrado.");
                    }
                   
                    _logger.LogInformation($"Inserindo o usuário {nameUser.ToUpper().Trim()} na tabela USERS_CX.");
                    await _userRepository.InsertUserData(nameUser, emailUser, ageUser, genderUser, passwordUser, officialNumberUser);
                    _logger.LogInformation($"Registro do usuário {nameUser.ToUpper().Trim()} feito com sucesso na tabela USERS_CX.");

                    controlProcess = true;

                    if (controlProcess)
                    {
                        var dto = new UserDTO() { UserName = nameUser, UserEmail = emailUser, DatePublisher = DateTime.Now };
                        _messageBusService.PublishMessage(dto);
                    }
                }
            }
            catch (Exception ex)
            {
                controlProcess = false;
                _logger.LogError($"[SaveNewResgisterUserData] - Erro ao inserir o usuário {nameUser.ToUpper().Trim()} no banco de dados: {ex.Message}");
                throw ex;

            }

            return controlProcess;
        }

        public async Task<Users> GetUserByEmail(string email)
        {
            try
            {
                var result = await _userRepository.GetAllUsers();
                var user = result.Where(x => x.UserEmail.ToUpper().Trim().Equals(email.ToUpper().Trim())).FirstOrDefault();

                return user ?? throw new Exception($"E-mail {email.ToUpper().Trim()} não encontrado.");
            }
            catch (Exception ex)
            {
                _logger.LogError($"[GetUserByEmail] - Erro ao consultar o usuário pelo e-mail {email.ToUpper().Trim()} no banco de dados: {ex.Message}");
                throw ex;
            }
        }

        public async Task<bool> UpdateUserRowData(int userID, string nameUser, string emailUser, int ageUser, string genderUser, string passwordUser, IFormFile? pictureUser, string officialNumberUser)
        {
            bool controlProcess = false;

            try
            {
                var listUsers = await _userRepository.GetAllUsers();

                if (listUsers == null || listUsers.Count() <= 0)
                {
                    _logger.LogInformation($"Tabela USERS_CX sem registros.");
                    return controlProcess;
                }
                else
                {
                    if (!listUsers.Any(x => x.UserID.Equals(userID)))
                    {
                        _logger.LogInformation($"Código {userID} não localizado na base.");
                        throw new ApplicationException($"Código {userID} não localizado na base.");
                    }

                    _logger.LogInformation($"Atualizando o usuário do código {userID} na tabela USERS_CX.");
                    await _userRepository.UpdateUserData(userID, nameUser, emailUser, ageUser, genderUser, passwordUser, pictureUser, officialNumberUser);
                    _logger.LogInformation($"Registro do usuário do código {userID} atualizado com sucesso na tabela USERS_CX.");

                    controlProcess = true;
                }
            }
            catch (Exception ex)
            {
                controlProcess = false;
                _logger.LogError($"[UpdateUserRowData] - Erro ao atualizar o usuário pelo código {userID} no banco de dados: {ex.Message}");
                throw ex;
            }

            return controlProcess;
        }

        public async Task<bool> DeleteUserRowData(int userID)
        {
            bool controlProcess = false;

            try
            {
                var listUsers = await _userRepository.GetAllUsers();

                if (listUsers == null || listUsers.Count() <= 0)
                {
                    _logger.LogInformation($"Tabela USERS_CX sem registros.");
                    return controlProcess;
                }
                else
                {
                    if (!listUsers.Any(x => x.UserID.Equals(userID)))
                    {
                        _logger.LogInformation($"Código {userID} não localizado na base.");
                        throw new ApplicationException($"Código {userID} não localizado na base.");
                    }

                    _logger.LogInformation($"Excluindo o usuário do código {userID} na tabela USERS_CX.");
                    await _userRepository.DeleteUserData(userID);
                    _logger.LogInformation($"Registro do usuário do código {userID} excluído com sucesso na tabela USERS_CX.");

                    controlProcess = true;
                }
            }
            catch (Exception ex)
            {
                controlProcess = false;
                _logger.LogError($"[DeleteUserRowData] - Erro ao excluir o usuário pelo código {userID} no banco de dados: {ex.Message}");
                throw ex;
            }

            return controlProcess;
        }
    }
}
