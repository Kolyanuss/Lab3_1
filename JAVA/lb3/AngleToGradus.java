import java.util.Scanner;

public class AngleToGradus {
    public static void main(String[] args) {
        Scanner scan = new Scanner(System.in);
        System.out.print("Enter Fist int: ");
        int first = scan.nextInt();
        System.out.print("Enter Second int: ");
        int second = scan.nextInt();

        double[] rez = calculate(first, second);
        System.out.println("rez:" + rez[0] + ' ' + rez[1]);
    }

    public static double[] calculate(int x, int y) {
        double hyp = Math.hypot(x, y);

        double rad1 = x/hyp;
        double rad2 = y/hyp;

        double[] rez = {Math.toDegrees(rad1), Math.toDegrees(rad2)};
        return rez;
    }
}
