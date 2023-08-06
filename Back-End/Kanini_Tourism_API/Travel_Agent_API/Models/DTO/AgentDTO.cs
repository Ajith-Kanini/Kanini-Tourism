namespace TravelAgencyManagementAPI.Models.DTO
{
    public class AgentDTO
    {
    
        public int AgentId { get; set; }

        public string? AgentName { get; set; }

     
        public string? AgentEmail { get; set; }


        public string? AgentPassword { get; set; }

        public string? AgentImage { get; set; }

        public string? AgentPhoneNumber { get; set; }

       

        public string? AgentCity { get; set; }

        public bool AgentStatus { get; set; }
    }
}
