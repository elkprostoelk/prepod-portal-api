using System.ComponentModel.DataAnnotations;
namespace PrepodPortal.Common.Enums;

public enum ArticleType
{
    [Display(Name = "���������� � ���������� �������� ������� �������� ������� ���")]
    WoSOrScopusLocal,
    [Display(Name = "���������� � ��������� �������� ��������, ������������ � �������������� ����� ����� Scopus ��/��� WoS")]
    WoSOrScopusForeign,
    [Display(Name = "���������� � �������� ������� �������� ������ ������� ���")]
    CategoryB,
    [Display(Name = "���������� � ����������� ��������")]
    ForeignEditionsNotB,
    [Display(Name = "���������� � ����� �������� ������")]
    PrintedInOtherUkraineEditions
}