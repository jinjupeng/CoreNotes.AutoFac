﻿using System;
using System.Collections.Generic;
using System.Text;

namespace CoreNotes.AutoFac.Model
{
    /// <summary>
    /// 实体基类
    /// </summary>
    public abstract class BaseEntity
    {
        /// <summary>唯一标识</summary>
        public long Id { get; set; }
    }
}
