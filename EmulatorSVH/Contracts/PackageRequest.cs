using System.ComponentModel.DataAnnotations;

namespace EmulatorSVH.Contracts
{
    public record PackageRequest
 (
     [Required]
        int Pid

 );

}
