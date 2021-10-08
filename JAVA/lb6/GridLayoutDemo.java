import java.awt.GridLayout;
import javax.swing.JButton;
import javax.swing.JFrame;
import javax.swing.JPanel;

public class GridLayoutDemo {
    public static void main(String[] args) {
        JFrame jf = new JFrame();
        jf.setDefaultCloseOperation(JFrame.EXIT_ON_CLOSE);
        jf.setSize(400, 300);
        jf.setVisible(true);
        // створюємо панель.
        JPanel p = new JPanel();
        jf.add(p);
        // до панелі додаємо менеджер GridLayout і встановлюємо розміри таблиці 3*3.
        p.setLayout(new GridLayout(3, 3));
        // до панелі додаємо кнопку і встановлюємо для неї менеджер у верхнє
        // розташування.
        p.add(new JButton("start 2"));
        p.add(new JButton("start 2"));
        p.add(new JButton("start 3"));
        p.add(new JButton("start 4"));
        p.add(new JButton("start 5"));
        p.add(new JButton("start 6"));
        p.add(new JButton("Okay"));
    }
}
