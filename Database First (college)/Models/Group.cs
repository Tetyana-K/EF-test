﻿using System;
using System.Collections.Generic;

namespace Database_First__college_.Models;

public partial class Group
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public int? DepartmentId { get; set; }

    public virtual Department? Department { get; set; } // віртуальний - для лінивого завантаження, якщо потрібно, навігаційна властивість для відношення з Department

    public virtual ICollection<Student> Students { get; set; } = new List<Student>(); // віртуальний - для лінивого завантаження, якщо потрібно, навігаційна властивість для відношення з Student
}
