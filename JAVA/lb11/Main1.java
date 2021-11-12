import java.awt.*;
import java.awt.event.*;
import javax.swing.*;
import javax.swing.border.Border;

public class Main1 extends JFrame {
    JPanel pane;

    JPanel jPInput;
    JPanel jPTxt;

    JPanel jPParamA;
    JPanel jPStep;
    JPanel jPPoint;

    JLabel jLTopinfo;
    JLabel jLParamA;
    JLabel jLStep;
    JLabel jLPoint;

    JTextField jTfParamA;
    JTextField jTfStep;
    JTextField jTfPoint;

    JTextArea txt;

    Main1() {
        super("Таблиця значень функції");
        try {
            UIManager.setLookAndFeel(UIManager.getSystemLookAndFeelClassName());
        } catch (Exception e) {
        }
        setSize(410, 300);

        // ________________ Initialize ________________
        pane = new JPanel();
        jPInput = new JPanel();
        jPTxt = new JPanel();
        jPParamA = new JPanel();
        jPStep = new JPanel();
        jPPoint = new JPanel();
        jLTopinfo = new JLabel("Функція: Y=A*sqrt(X)*Sin(A*X)");
        jLParamA = new JLabel("Параметр А ");
        jLStep = new JLabel("Шаг(h):");
        jLPoint = new JLabel("Кількість точок:");
        jTfParamA = new JTextField(10);
        jTfStep = new JTextField(5);
        jTfPoint = new JTextField(5);
        txt = new JTextArea(9, 75);
        Container c = getContentPane();

        // ________________ add ________________
        c.add(pane, BorderLayout.CENTER);

        pane.add(jLTopinfo);
        pane.add(jPInput);
        pane.add(jPTxt);

        jPInput.add(jPParamA, BorderLayout.NORTH);
        jPInput.add(jPStep, BorderLayout.CENTER);
        jPInput.add(jPPoint, BorderLayout.EAST);

        jPParamA.add(jLParamA);
        jPParamA.add(jTfParamA);
        jPStep.add(jLStep);
        jPStep.add(jTfStep);
        jPPoint.add(jLPoint);
        jPPoint.add(jTfPoint);

        jPTxt.add(txt, BorderLayout.SOUTH);
        JScrollPane scroll = new JScrollPane(txt);
        jPTxt.add(scroll);

        // ________________ set ________________
       
        System.out.println(c.getPreferredSize());
        System.out.println(pane.getPreferredSize());
        System.out.println(jPTxt.getPreferredSize());
        jPInput.setPreferredSize(new Dimension(390, 100));
        jPInput.setBorder(BorderFactory.createTitledBorder("Вхідні дані"));

        setResizable(false);
        // ________________ func ________________
        jTfParamA.addActionListener(new ActionListener() {
            public void actionPerformed(ActionEvent e) {
                calc();
            }
        });
        jTfStep.addActionListener(new ActionListener() {
            public void actionPerformed(ActionEvent e) {
                calc();
            }
        });
        jTfPoint.addActionListener(new ActionListener() {
            public void actionPerformed(ActionEvent e) {
                calc();
            }
        });

        // ________________ end ________________
        WindowListener wndCloser = new WindowAdapter() {
            public void windowClosing(WindowEvent e) {
                System.exit(0);
            }
        };
        addWindowListener(wndCloser);
        setVisible(true);
    }

    // ________________ func ________________
    public void calc() {
        double y;
        double x = Double.parseDouble(this.jTfStep.getText());
        double x1 = x;
        int n = Integer.parseInt(this.jTfPoint.getText());
        double a = Double.parseDouble(this.jTfParamA.getText());
        for (int i = 0; i < n; i++) {
            y = a * Math.sqrt(x1) * Math.sin(a * x1);
            printToText(x1, y);
            x1 += x;
        }
    }

    public void printToText(double x, double y) {
        txt.append(x + "\t" + y + "\n");
    }

    public static void main(String[] args) {
        Main1 d = new Main1();
        d.calc();
    }
}
