namespace NumberValidator.Validators
{
    public interface IValidator
    {
        bool IsValid(string input);
        void Validate(string input);
    }
}
