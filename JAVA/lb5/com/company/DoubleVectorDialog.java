package com.company;

import java.awt.*;
import java.awt.event.*;
import javax.swing.*;

public class DoubleVectorDialog {
    protected JFrame frm = new JFrame("DoubleVectorDialog");
    protected Container cont;

    public DoubleVectorDialog() {
        try {
            UIManager.setLookAndFeel(UIManager.getSystemLookAndFeelClassName());
        } catch (Exception e) {
        }
        frm.setSize(300, 200);
        cont = frm.getContentPane();

        WindowListener wndCloser = new WindowAdapter() {
            public void windowClosing(WindowEvent e) {
                System.exit(0);
            }
        };
        frm.addWindowListener(wndCloser);
        frm.setMinimumSize(new Dimension(150, 100));
        // frm.setMaximumSize(new Dimension(400, 400)); dont work

        frm.setIconImage(new ImageIcon(".\\.\\WOT.jpg").getImage());

        frm.setName("My name"); // how it work?
        cont.setBackground(Color.YELLOW);

        frm.setVisible(true);
        cont.setLayout(new FlowLayout());
    }

    public void print3el(double[] a, double[] b, double[] c) {
        DoubleVector v1 = new DoubleVector(a);
        DoubleVector v2 = new DoubleVector(b);
        DoubleVector v3 = new DoubleVector(c);

        cont.add(new JLabel("v1 * v2 = " + v1.mult(v2)));
        cont.add(new JLabel("v1 * v2 = " + DoubleVector.mult(v1, v2)));
        cont.add(new JLabel("v1 * v3 = " + v1.mult(v3)));
    }

    public static void main(String[] args) {
        DoubleVectorDialog vec1 = new DoubleVectorDialog();

        double[] a = { 1, 2, 3, 4 };
        double[] b = { 1, 1, 1, 1 };
        double[] c = { 2, 2, 2, 2 };

        vec1.print3el(a, b, c);
    }
}
