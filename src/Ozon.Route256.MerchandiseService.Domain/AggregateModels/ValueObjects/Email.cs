using System.Collections.Generic;
using System.Net.Mail;
using Ozon.Route256.MerchandiseService.Domain.BaseModels;
using Ozon.Route256.MerchandiseService.Domain.Exceptions.EmployeeAggregate;

namespace Ozon.Route256.MerchandiseService.Domain.AggregateModels.ValueObjects
{
    public class Email : ValueObject
    {
        public string Value { get; internal set; }

        public Email(string email)
        {
            SetEmail(email);
        }

        private void SetEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                throw new InvalidEmailException($"Invalid email address: passed null value");
            }
            
            // не самый удачный пример для валидации, в будущем нужно будет поправить
            if (!MailAddress.TryCreate(email, out _))
            {
                throw new InvalidEmailException($"Invalid email address: {email}");
            }

            Value = email;
        }
        
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}