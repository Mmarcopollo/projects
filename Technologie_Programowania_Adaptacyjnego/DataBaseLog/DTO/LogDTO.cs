﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseLog.DTO
{
        [Table("Log")]
        public class LogDTO
        {

            [Key]
            public int ID { get; set; }
            public string Message { get; set; }
            public DateTime Time { get; set; }

            public LogDTO() { }

            public LogDTO(string message, DateTime dateTime)
            {
                Message = message;
                Time = dateTime;
            }
        }
}
