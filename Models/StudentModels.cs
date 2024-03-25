namespace OneToManyMapping_10_03_24.Models
{
    public class StudentModels
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
        public string City { get; set; }
        public string Qualification { get; set; }
        public string University { get; set; }
        public int PassingYear { get; set; }
        public float Percentage { get; set; }
    }



    public class ResponseModel
    {
        public string ResponseMsg { get; set; }
        public bool OperationSuccess { get; set; }
        public int InsertedStudentID { get; set; }
    }
}
