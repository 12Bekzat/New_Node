﻿namespace Shop.Domain
{
  public class User : Entity
  {
    public string Name { get; set; }
    public string Surname { get; set; }
    public string PhoneNumber { get; set; }
    public string Email { get; set; }
    public string Address { get; set; }
    public string Password { get; set; }
    public string VerificationCode { get; set; }

    //покупки, коментарий, рейтинги и т.д
  }
}
