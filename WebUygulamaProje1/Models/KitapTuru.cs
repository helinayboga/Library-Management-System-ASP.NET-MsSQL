﻿using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace WebUygulamaProje1.Models
{
	public class KitapTuru
	{
		[Key]
		public int Id { get; set; }
		[Required(ErrorMessage ="Kitap türü adı boş bırakılamaz.") ]
		[MaxLength(25)]
		[DisplayName("Kitap Türü Adı")]
		public string Ad { get; set; }
	}
}
