using Sample.Application.Lib.Model.Domain;
using System.Threading.Tasks;

namespace Sample.Application.Lib.Infra.Contract
{
    /// <summary>
    /// The AppointmentCreate Validate
    /// </summary>
    public interface IMessageValidate
    {
        /// <summary>
        /// Validates the specified request.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        Task<bool> Validate(EmailMessage request);
    }
}
