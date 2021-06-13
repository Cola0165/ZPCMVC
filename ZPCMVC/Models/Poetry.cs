using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web;

namespace ZPCMVC.Models
{
    public class Poetry
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "诗名")]
        [Required(ErrorMessage = "这是必须项")]
        [DataType(DataType.Text)]
        public string Name { get; set; }

        [Display(Name = "朝代")]
        [DataType(DataType.Text)]
        public string Dynasty { get; set; }

        [Display(Name = "作者")]
        [DataType(DataType.Text)]
        public string Poet { get; set; }

        [Display(Name ="诗文")]
        [DataType(DataType.MultilineText)]

        public string Cont { get; set; }

        public string Location { get; set; }
    }
}