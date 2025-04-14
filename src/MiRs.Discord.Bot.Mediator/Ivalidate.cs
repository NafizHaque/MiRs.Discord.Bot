using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiRs.Discord.Bot.Mediator
{
    /// <summary>
    /// Interface for adding Validation to Request objects used by MediatR.
    /// </summary>
    public interface IValidatable
    {
        /// <summary>
        /// Validate method used to impliment Validations on request arguments.
        /// </summary>
        void Validate();
    }
}
