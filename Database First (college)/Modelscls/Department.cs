using System;
using System.Collections.Generic;

namespace Database_First__college_.Modelscls;

public partial class Department
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Group> Groups { get; set; } = new List<Group>();
}
