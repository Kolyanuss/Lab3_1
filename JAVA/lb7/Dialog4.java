import java.awt.*;
import java.awt.event.*;
import javax.swing.*;

import java.awt.datatransfer.StringSelection;
import java.awt.Toolkit;
import java.awt.datatransfer.Clipboard;

public class Dialog4 extends JFrame {
    JPanel panelFirst = new JPanel();
    JPanel panelInput = new JPanel();
    JPanel panelOutput = new JPanel();
    JPanel panelButton = new JPanel();

    JLabel lblInfo = new JLabel(" ");
    JLabel lbl = new JLabel("Поле вводу");
    JLabel lbl2 = new JLabel("Поле виводу");
    JTextField fld = new JTextField(20);
    JTextField fld2 = new JTextField(20);
    JButton btn = new JButton("Скопіювати");

    Dialog4() {
        super("Слухачі (listeners) полів та кнопок");
        try {
            UIManager.setLookAndFeel(UIManager.getSystemLookAndFeelClassName());
        } catch (Exception e) {
        }
        setSize(400, 150);
        Container c = getContentPane();
        c.add(panelFirst, BorderLayout.CENTER);
        c.add(panelButton, BorderLayout.SOUTH);

        panelFirst.add(panelInput, BorderLayout.NORTH);
        panelFirst.add(panelOutput, BorderLayout.SOUTH);

        panelInput.add(lbl, BorderLayout.WEST);
        panelInput.add(fld, BorderLayout.EAST);

        panelOutput.add(lbl2, BorderLayout.WEST);
        panelOutput.add(fld2, BorderLayout.EAST);

        panelButton.add(btn, BorderLayout.WEST);
        panelButton.add(lblInfo, BorderLayout.EAST);

        fld.addActionListener(new ActionListener() {
            public void actionPerformed(ActionEvent e) {
                if (fld.getText().length() > 0) {
                    fld2.setText("❤" + fld.getText() + "❤");
                } else
                    fld2.setText("");
                lblInfo.setText("Скопійовано через Enter");
            }
        });

        btn.addActionListener(new ActionListener() {
            public void actionPerformed(ActionEvent e) {
                if (fld.getText().length() > 0) {
                    fld2.setText("❤" + fld.getText() + "❤");
                    // copy to clipboard
                    StringSelection stringSelection = new StringSelection(fld2.getText());
                    Clipboard clipboard = Toolkit.getDefaultToolkit().getSystemClipboard();
                    clipboard.setContents(stringSelection, null);
                    // copy to clipboard
                } else
                    fld2.setText("");
                lblInfo.setText("Скопійовано в буфер-обміну");
            }
        });
        WindowListener wndCloser = new WindowAdapter() {
            public void windowClosing(WindowEvent e) {
                System.exit(0);
            }
        };
        addWindowListener(wndCloser);
        setVisible(true);
    }

    public static void main(String[] args) {
        new Dialog4();
    }
}