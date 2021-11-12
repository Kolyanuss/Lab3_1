import java.awt.*;
import java.awt.event.*;
import javax.swing.*;

public class TArea extends JFrame {
    JTextArea txt;

    TArea() {
        super("Візуальнезастосування");
        try {
            UIManager.setLookAndFeel(UIManager.getSystemLookAndFeelClassName());
        } catch (Exception e) {
        }
        setSize(410, 300);
        Container c = getContentPane();
        JPanel pane = new JPanel();
        c.add(pane, BorderLayout.CENTER);
        pane.add(new JLabel("Hello, привіт"));
        pane.add(txt = new JTextArea(15, 75));
        JScrollPane scroll = new JScrollPane(txt);
        pane.add(scroll);
        WindowListener wndCloser = new WindowAdapter() {
            public void windowClosing(WindowEvent e) {
                System.exit(0);
            }
        };
        addWindowListener(wndCloser);
        setVisible(true);
    }

    public void test() {
        txt.append("Першийрядок\n");
        txt.append("Другий рядок\n");
    }

    public static void main(String[] args) {
        TArea d = new TArea();
        d.test();
    }
}