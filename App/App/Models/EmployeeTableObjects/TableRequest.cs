using App.Models.ManagingTableModels;

namespace App.Models.JqGridObjects
{
    public class TableRequest
    {
        public bool IsSearch { get; set; }

        public int Page { get; set; }

        public int Rows { get; set; }

        public string SortingProperty { get; set; }

        public SortEnum SortOrder { get; set; }

        public string Filters { get; set; } //parse

        public string Name { get; set; }

        public string Surname { get; set; }

        public Roles Role { get; set; }

        public int? Id { get; set; }

        public int? ProjectId { get; set; }

        public TableRequest()
        {
            Name = "";
            Surname = "";
            Role = Roles.All;
            Id = null;
        }
    }
}