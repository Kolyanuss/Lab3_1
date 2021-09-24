import java.util.Scanner;

public class SymbolTest {
    public static void main(String[] args) {
        System.out.println("Enter int");
        Scanner scan = new Scanner(System.in);
        int b = Integer.parseInt(scan.nextLine());
        symbol_rand(b);
    }

    public static void symbol_rand(int x) {
        for (int i = 0; i < x; i++) {
            char c = (char) (Math.random() * 26 + 'a');
            System.out.print(c + ": ");
            switch (c) {
                case 'a':
                case 'e':
                case 'i':
                case 'o':
                case 'u':
                    System.out.println("гласная");
                    break;
                case 'y':
                case 'w':
                    System.out.println("иногда гласная");
                    break;
                default:
                    System.out.println("согласная");
            }
        }
    }
}
