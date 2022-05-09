using System.Collections.Generic;

namespace BankApp.Validations
{
    public interface IValidator<in TSource>
    {
        LinkedList<string> Validate(TSource source);
    }
}
