import java.awt.GridLayout;
import java.awt.event.ActionEvent;
import java.awt.event.ActionListener;
import java.util.ArrayList;
import java.util.List;

import javax.swing.AbstractButton;
import javax.swing.ButtonGroup;
import javax.swing.JFrame;
import javax.swing.JLabel;
import javax.swing.JPanel;
import javax.swing.JRadioButton;

public class tests {
    public static void main(String args[]) {
        ArrayList<String> questions = new ArrayList<String>();
        // questions

        JFrame frame = new JFrame("Grouping Example");
        frame.setDefaultCloseOperation(JFrame.EXIT_ON_CLOSE);
        JPanel panel = new JPanel(new GridLayout(0, 1));
        JLabel lableAnsver = new JLabel("Як справи?");
        ButtonGroup group = new ButtonGroup();
        JRadioButton aRadioButton = new JRadioButton("A");
        JRadioButton bRadioButton = new JRadioButton("Б");
        JRadioButton cRadioButton = new JRadioButton("B");
        JRadioButton dRadioButton = new JRadioButton("Г");
        ActionListener sliceActionListener = new ActionListener() {
            public void actionPerformed(ActionEvent actionEvent) {
                AbstractButton aButton = (AbstractButton) actionEvent.getSource();
                System.out.println("Selected: " + aButton.getText());
            }
        };
        panel.add(lableAnsver);
        panel.add(aRadioButton);
        group.add(aRadioButton);
        panel.add(bRadioButton);
        group.add(bRadioButton);
        panel.add(cRadioButton);
        group.add(cRadioButton);
        panel.add(dRadioButton);
        group.add(dRadioButton);


        aRadioButton.addActionListener(sliceActionListener);
        bRadioButton.addActionListener(sliceActionListener);
        frame.add(panel);
        frame.setSize(300, 200);
        frame.setVisible(true);
    }
}
