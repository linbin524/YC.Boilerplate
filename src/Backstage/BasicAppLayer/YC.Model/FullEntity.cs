﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using FreeSql;

namespace YC.Model.DbEntity
{
    public class FullEntity<T> : BaseEntity<T>
    {
        [DisplayName(" 最后编辑时间")]
        public DateTime LastModificationTime { get; set; }
        [DisplayName(" 最后编辑人ID")]
        public T LastModifierUserId { get; set; }
        [DisplayName(" 删除时间")]
        public DateTime? DeletionTime { get; set; }
        [DisplayName(" 最后删除人ID")]
        public T DeleterUserId { get; set; }
        [DisplayName(" 创建时间")]
        public DateTime? CreationTime { get; set; }
        [DisplayName(" 创建ID")]
        public T CreatorUserId { get; set; }

    }
}
