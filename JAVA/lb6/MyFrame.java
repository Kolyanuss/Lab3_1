import java.awt.*;
import java.awt.Insets;
import javax.swing.*;
import javax.swing.border.EmptyBorder;

public class MyFrame {

    public static void main(String[] args) {
        JFrame jf = new JFrame();
        jf.setDefaultCloseOperation(JFrame.EXIT_ON_CLOSE);
        // jf.setSize(280, 150); //dont work

        // CREATE
        Container boxGlobal = jf.getContentPane();
        Box boxLog = Box.createHorizontalBox();
        Box boxPas = Box.createHorizontalBox();
        Box boxBut = Box.createHorizontalBox();

        boxGlobal.setLayout(new BoxLayout(boxGlobal, BoxLayout.Y_AXIS));

        JLabel jLLogin = new JLabel("Логін:");
        JLabel jLPass = new JLabel("Пароль:");
        JTextField jFLogin = new JTextField(15);
        JTextField jFPass = new JPasswordField(15);
        JButton jButOk = new JButton("OK");
        JButton jButCancel = new JButton("Відміна");

        // PARAM
        jFLogin.setMargin(new Insets(0, 10, 0, 10));
        jFPass.setMargin(new Insets(0, 10, 0, 10));

        jLLogin.setPreferredSize(jLPass.getPreferredSize());

        // ADD
        boxLog.add(jLLogin);
        boxLog.add(Box.createHorizontalStrut(6));
        boxLog.add(jFLogin);

        boxPas.add(jLPass);
        boxPas.add(Box.createHorizontalStrut(6));
        boxPas.add(jFPass);

        boxBut.add(Box.createHorizontalGlue());
        boxBut.add(jButOk);
        boxBut.add(Box.createHorizontalStrut(12));
        boxBut.add(jButCancel);

        boxGlobal.setBounds(12, 12, 12, 12);

        boxLog.setBorder(new EmptyBorder(12, 12, 0, 12));
        boxPas.setBorder(new EmptyBorder(0, 12, 0, 12));
        boxBut.setBorder(new EmptyBorder(0, 12, 12, 12));

        boxGlobal.add(boxLog);
        boxGlobal.add(Box.createVerticalStrut(12));
        boxGlobal.add(boxPas);
        boxGlobal.add(Box.createVerticalStrut(17));
        boxGlobal.add(boxBut);

        jf.pack();
        jf.setResizable(false);
        jf.setVisible(true);
    }
}
