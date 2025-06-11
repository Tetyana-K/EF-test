using System;
using System.Collections.Generic;

namespace Database_First__college_.Modelscls;

public partial class Group
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public int? DepartmentId { get; set; }

    public virtual Department? Department { get; set; }

    public virtual ICollection<Student> Students { get; set; } = new List<Student>();
}
