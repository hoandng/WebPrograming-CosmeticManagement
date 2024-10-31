using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace QuanLyBanMyPham.Models;

public partial class Order
{
    public int OrderId { get; set; }

    public int? UserId { get; set; }

    [DataType(DataType.Date)]
    public DateOnly? OrderDate { get; set; }

    [Range(0.01, double.MaxValue, ErrorMessage = "Số tiền tổng phải lớn hơn 0.")]
    public decimal? TotalAmount { get; set; }

    [StringLength(50, ErrorMessage = "Trạng thái không được vượt quá 50 ký tự.")]
    public string? Status { get; set; }

    public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();

    public virtual User? User { get; set; }
}
