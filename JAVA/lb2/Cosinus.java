import java.lang.*;

public class Cosinus {
    public static void main(String[] args) {
        // get two double numbers
        double x = 30.0;
        double y = 30.0;
        // convert them to radians
        x = Math.toRadians(x);
        y = Math.toRadians(y);
        x = Math.cos(x);
        y = Math.sin(y);
        // print their cosine
        System.out.println("cos 30° = " + x);
        System.out.println("sin 30° = " + y);
    }
}