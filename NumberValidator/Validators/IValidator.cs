namespace NumberValidator.Validators
{
    public interface IAadhaarValidator
    {
        bool IsValid(string input);
        void Validate(string input);
    }
}
