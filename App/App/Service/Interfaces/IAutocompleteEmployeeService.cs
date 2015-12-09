namespace App.Service.Interfaces
{
    public interface IAutocompleteEmployeeService
    {
        string FormAutocompleteResponseByName(string query);

        string FormAutocompleteResponseBySurname(string query);
    }
}
