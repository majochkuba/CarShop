using System.ComponentModel.DataAnnotations;

namespace CarShop.Models;

public class Result
{
    public int MakeId { get; set; }
    public string MakeName { get; set; }
    public int VehicleTypeId { get; set; }
    public string VehicleTypeName { get; set; }
}

public class VechiclesModel
{
    public int Count { get; set; }
    public string Message { get; set; }
    public string SearchCriteria { get; set; }
    public List<Result> Results { get; set; }
}
