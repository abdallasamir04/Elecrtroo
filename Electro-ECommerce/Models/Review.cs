using System;
using System.Collections.Generic;

namespace Electro_ECommerce.Models;

public partial class Review
{
    public int ReviewId { get; set; }

    public int ProductId { get; set; }

    public string UserName { get; set; } = null!;

    public int Rating { get; set; }

    public string? Comment { get; set; }

    public DateTime Date { get; set; }

    public virtual Product Product { get; set; } = null!;
}
