import java.awt.*;
import java.awt.event.*;
import java.util.TreeSet;

import javax.swing.*;

public class Zapysnyk extends JFrame {

    JPanel panelFirst = new JPanel();
    JPanel panelSecond = new JPanel();

    JPanel grid = new JPanel();

    JLabel lbl1 = new JLabel("Фамілія");
    JLabel lbl2 = new JLabel("Телефон");
    JTextField fld1 = new JTextField(20);
    JTextField fld2 = new JTextField(20);
    JButton btndruc = new JButton("Друкувати");

    JLabel lblInfo = new JLabel("Кількість записів: ");
    JTextField fldInfo = new JTextField("0", 5);

    TreeSet<String> myset1 = new TreeSet<String>();

    private void calc() {
        var str = fld1.getText();
        var str2 = fld2.getText();
        if (str.length() > 0 && str2.length() > 0) {
            myset1.add(str + " " + str2);
        }
        fldInfo.setText("" + myset1.size());
    }

    Zapysnyk() {
        super("Записник");
        try {
            UIManager.setLookAndFeel(UIManager.getSystemLookAndFeelClassName());
        } catch (Exception e) {
        }
        setLocation(800, 400);
        Container c = getContentPane();

        grid.setLayout(new GridLayout(3, 2, 5, 20));

        c.add(panelFirst, BorderLayout.CENTER);
        c.add(panelSecond, BorderLayout.SOUTH);

        panelFirst.add(grid);
        panelSecond.add(lblInfo);
        panelSecond.add(fldInfo);

        grid.add(lbl1);
        grid.add(fld1);
        grid.add(lbl2);
        grid.add(fld2);
        grid.add(new JPanel());
        grid.add(btndruc);

        // --------edit param----------

        fldInfo.setEditable(false);

        panelFirst.setBorder(BorderFactory.createCompoundBorder(BorderFactory.createRaisedBevelBorder(),
                BorderFactory.createLoweredBevelBorder()));
        panelSecond.setBorder(BorderFactory.createCompoundBorder(BorderFactory.createRaisedBevelBorder(),
                BorderFactory.createLoweredBevelBorder()));

        
        // --------func----------

        fld1.addActionListener(new ActionListener() {
            public void actionPerformed(ActionEvent e) {
                calc();
            }
        });

        fld2.addActionListener(new ActionListener() {
            public void actionPerformed(ActionEvent e) {
                calc();
            }
        });

        btndruc.addActionListener(new ActionListener() {
            public void actionPerformed(ActionEvent e) {
                System.out.println("Start print:");
                for (String x : myset1) {
                    System.out.println(x);
                }
            }
        });

        WindowListener wndCloser = new WindowAdapter() {
            public void windowClosing(WindowEvent e) {
                System.exit(0);
            }
        };

        addWindowListener(wndCloser);
        pack();
        setVisible(true);
    }

    public static void main(String[] args) {
        new Zapysnyk();
    }
}