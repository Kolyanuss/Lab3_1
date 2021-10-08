import java.awt.FlowLayout;
import javax.swing.JButton;
import javax.swing.JFrame;
import javax.swing.JPanel;

public class FlowLayoutDemo {
    public static void main(String[] args) {
        // створюємо вікно і встановлюємо його розмір.
        JFrame jf = new JFrame();
        jf.setDefaultCloseOperation(JFrame.EXIT_ON_CLOSE);
        jf.setSize(400, 300);
        jf.setVisible(true);
        // створюємо панель.
        JPanel p = new JPanel();
        jf.add(p);
        // до панелі додаємо менеджер FlowLayout.
        p.setLayout(new FlowLayout());
        // до панелі додаємо кнопки.
        p.add(new JButton("start 2"));
        p.add(new JButton("start 2"));
        p.add(new JButton("start 3"));
        p.add(new JButton("start 4"));
        p.add(new JButton("start 5"));
        p.add(new JButton("start 6"));
        p.add(new JButton("Okay"));
    }
}
