﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core;

namespace Commands.News
{
	public class UpdateActivationCommand : Command
	{
		public Guid NewsId { get; set; }
        public bool IsActive { get; set; }
	}
}
