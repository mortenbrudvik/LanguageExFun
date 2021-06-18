namespace FunConsole.CarDataModel
{
    public record Car(Brand Brand, Color Color, int Year, int Price, string Description = "");
    
    public enum Brand{Volvo, Fiat, Toyota, Ford
    }
    public enum Color{Red, Blue, Yellow}
}