using System.ComponentModel.DataAnnotations;
namespace PrepodPortal.Common.Enums;

public enum ArticleType
{
    [Display(Name = "Надруковані в вітчизняних наукових фахових виданнях категорії «А»")]
    WoSOrScopusLocal,
    [Display(Name = "Надруковані в зарубіжних наукових виданнях, індексованих у наукометричних базах даних Scopus та/або WoS")]
    WoSOrScopusForeign,
    [Display(Name = "Надруковані в наукових фахових виданнях України категорії «Б»")]
    CategoryB,
    [Display(Name = "Надруковані в закордонних виданнях")]
    ForeignEditionsNotB,
    [Display(Name = "Надруковані в інших виданнях України")]
    PrintedInOtherUkraineEditions
}