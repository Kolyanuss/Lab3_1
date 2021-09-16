import java.lang.*;
import java.io.*;

public class Cosinus1 {
    public static void main(String[] args) throws IOException {
        System.out.println("Please, input angel:");
        // створюємо нове посилання a на класс BufferedReader
        BufferedReader a = new BufferedReader(new InputStreamReader(System.in));
        // привласнюємо змінній angle типу string значення a
        String angle = a.readLine();
        // перетворюємо строковий тип змінної angle у тип double
        double x = Double.parseDouble(angle);
        // перетворюємо в радіани
        x = Math.toRadians(x);
        // розраховуємо значення косинуса та синуса
        double cos = Math.cos(x);
        double sin = Math.sin(x);
        // друк значень косинуса на синуса
        System.out.println("cos = " + cos);
        System.out.println("sin = " + sin);
    }
}