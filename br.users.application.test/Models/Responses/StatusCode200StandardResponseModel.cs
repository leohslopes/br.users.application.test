using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace br.users.application.test.Api.Models.Responses
{
    public class StatusCode200StandardResponseModel
    {
        public StatusCode200StandardResponseModel()
        {
           Errors = new Dictionary<string, string>();
        }
        public StatusCode200StandardResponseModel(FluentValidation.ValidationException ve)
        {
            Success = false;
            Errors = new Dictionary<string, string>();
            var errorsList = ve.Errors.ToList();

            if (!string.IsNullOrEmpty(ve.Message) && !errorsList.Any())
            {
                ValidationFailure error = new ValidationFailure("errorMessage", ve.Message);
                error.ErrorCode = "errorCode";
                errorsList.Add(error);
            }


            if (errorsList.Count > 0)
            {
                foreach (ValidationFailure item in errorsList)
                {
                    this.Errors.Add(new KeyValuePair<string, string>($"{item.PropertyName}_{item.ErrorCode}", item.ErrorMessage));
                }
            }
        }

        [Required]
        public bool? Success { get; set; } = true;
        public IDictionary<string, string> Errors { get; set; }
    }
}
