namespace PrepodPortal.Common.Configurations;

public class EmailConfiguration
{
    public string UserName { get; set; }
    
    public string Password { get; set; }
    
    public string Host { get; set; }
    
    public int Port { get; set; }
    
    public bool UseDefaultCredentials { get; set; }
    
    public bool EnableSsl { get; set; }

    public System.Net.Mail.SmtpDeliveryMethod DeliveryMethod { get; set; }
}