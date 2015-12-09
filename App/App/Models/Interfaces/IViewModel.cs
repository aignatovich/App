namespace App.Models
{
    interface IViewModel<Type>
    {
        Type ConvertToModel();
    }
}
