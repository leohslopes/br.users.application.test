using br.users.application.test.domain.Entities.UserCx;
using br.users.application.test.domain.Interfaces.Repositories;
using br.users.application.test.domain.Interfaces.Services;
using Microsoft.Extensions.Logging;
using MySqlX.XDevAPI.Common;

namespace br.users.application.test.application.Services
{
    public class UserService: BaseService, IUserService
    {
        private readonly ILogger<UserService> _logger;
        private readonly IUserCxRepository _userRepository;

        public UserService(ILogger<UserService> logger, IUserCxRepository userRepository)
        {
            _logger = logger;
            _userRepository = userRepository;
        }

       public async Task<IEnumerable<Users>> GetItemsUserList()
        {
            IEnumerable<Users> result;

            try
            {
                result = await _userRepository.GetAllUsers();
            }
            catch (ApplicationException ex)
            {
                _logger.LogError($"[GetItemsUserList] - Erro ao consultar os usuários no banco de dados: {ex.Message}");
                throw ex;
            }

            return result;
        }

        public async Task<bool> SaveNewResgisterUserData(string nameUser, string emailUser, int ageUser, string genderUser, string passwordUser)
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
                    await _userRepository.InsertUserData(nameUser, emailUser, ageUser, genderUser, passwordUser);
                    _logger.LogInformation($"Registro do usuário {nameUser.ToUpper().Trim()} feito com sucesso na tabela USERS_CX.");

                    controlProcess = true;
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

                if (user == null)
                {
                    throw new Exception($"E-mail {email.ToUpper().Trim()} não encontrado.");
                }

                return user;
                    
            }
            catch (Exception ex)
            {
                _logger.LogError($"[GetUserByEmail] - Erro ao consultar o usuário pelo e-mail{email.ToUpper().Trim()} no banco de dados: {ex.Message}");
                throw ex;
            }
        }
    }
}
