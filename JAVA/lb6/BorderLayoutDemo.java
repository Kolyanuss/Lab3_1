import java.awt.BorderLayout;
import javax.swing.JButton;
import javax.swing.JFrame;
import javax.swing.JPanel;

public class BorderLayoutDemo {
    public static void main(String[] args) {
        // створюємо фрейм і встановлюємо його розмір.
        JFrame jf = new JFrame();
        jf.setDefaultCloseOperation(JFrame.EXIT_ON_CLOSE);
        jf.setSize(400, 300);
        jf.setVisible(true);
        // створюємо панель.
        JPanel p = new JPanel();
        jf.add(p);
        // до панелі додаємо менеджер BorderLayout.
        p.setLayout(new BorderLayout());
        // до панелі додаємо кнопку і встановлюємо для неї менеджер у верхнє
        // розташування.
        p.add(new JButton("Okay"), BorderLayout.NORTH);
    }
}