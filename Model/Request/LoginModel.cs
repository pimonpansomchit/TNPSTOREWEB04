using System.ComponentModel.DataAnnotations;

namespace TNPSTOREWEB.Models.Request
{
    public partial class LoginModel
    {
        [Required(ErrorMessage = "ไม่สามารถใส่ค่าว่างได้ กรุณาใช้ username ให้ครบถ้วน")]
        public string USERNAME { get; set; } = null!;
        [Required(ErrorMessage = "ไม่สามารถใส่ค่าว่างได้ กรุณาใช้ password ให้ครบถ้วน")]
        public string PASSWORD { get; set; } = null!;

    }
}
