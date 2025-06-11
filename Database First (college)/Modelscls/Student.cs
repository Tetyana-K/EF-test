using System;
using System.Collections.Generic;

namespace Database_First__college_.Modelscls;

public partial class Student
{
    public int Id { get; set; }

    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public int? GroupId { get; set; }

    public int? Course { get; set; }

    public virtual Group? Group { get; set; }
}
