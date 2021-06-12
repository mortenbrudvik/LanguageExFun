namespace FunConsole.CarDataModel
{
    public record Car(Brand Brand, Color Color, int Year, int Price, string Description = "");
    
    public enum Brand{Volvo, Fiat, Toyota}
    public enum Color{Red, Blue}
}