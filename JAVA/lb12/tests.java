import java.awt.event.ActionListener;
import java.awt.event.ActionEvent;
import java.awt.BorderLayout;
import java.awt.GridLayout;
import java.util.ArrayList;
import java.util.HashMap;
import java.util.Arrays;
import java.util.Map;

import javax.swing.border.EtchedBorder;
import javax.swing.AbstractButton;
import javax.swing.BorderFactory;
import javax.swing.JRadioButton;
import javax.swing.ButtonGroup;
import javax.swing.JButton;
import javax.swing.JFrame;
import javax.swing.JLabel;
import javax.swing.JPanel;

public class tests {
    int currentQuestions = 0;
    int currentAnswer = 0;
    int totalScore = 0;

    ArrayList<String> questions;
    ArrayList<Integer> answersToQuestions;
    ArrayList<Integer> complitedQuestions;
    Map<Integer, ArrayList<String>> answerList;

    public void initialize() {
        questions = new ArrayList<String>() {
            {
                add("Хто був першим президентом США?");
                add("Яка молярна маса пропану?");
                add("Скільки років нашому всесвіту?");
                add("Дата висадки першої людини на місяць?");
                add("Три основні принципи ООП?");
                add("Кількість континентів на планеті Земля?");
            }
        };
        answersToQuestions = new ArrayList<Integer>(Arrays.asList(1, 2, 0, 3, 0, 3));

        answerList = new HashMap<Integer, ArrayList<String>>();
        answerList.put(0, new ArrayList<String>() {
            {
                add("Авраам Лінкольн");
                add("Джордж Вашингтон");
                add("Теодор Рузвельт");
                add("Джо Байден");
            }
        });
        answerList.put(1, new ArrayList<String>() {
            {
                add("58,12 г/моль");
                add("16,04 г/моль");
                add("44,1 г/моль"); // +
                add("30,07 г/моль");
            }
        });
        answerList.put(2, new ArrayList<String>() {
            {
                add("13.7±3 мільярдів років");
                add("2021 років");
                add("14,46±0,8 мільярдів років");
                add("16,78±0,5 мільярдів років");
            }
        });
        answerList.put(3, new ArrayList<String>() {
            {
                add("4 жовтня 1957 року"); // перший спутник
                add("12 квітня 1961 р"); // перший політ людини в космос
                add("19 березня 1965 року"); // перший вихід в космос
                add("20 липня 1969 року"); // перший вихід на місяць
            }
        });
        answerList.put(4, new ArrayList<String>() {
            {
                add("Інкапсулція, Унаслідування, Поліморфізм");
                add("Агрегація, Абстрактність, Класовість");
                add("Модифікація, Розгалуження, Розширення");
                add("Ні одне з перелічених вище");
            }
        });
        answerList.put(5, new ArrayList<String>() {
            {
                add("1");
                add("5");
                add("6");
                add("7");
            }
        });

        JFrame frame = new JFrame("Grouping Example");
        frame.setDefaultCloseOperation(JFrame.EXIT_ON_CLOSE);
        JPanel panel = new JPanel(new GridLayout(0, 1));
        JPanel panel2 = new JPanel();
        JLabel lableQuestions = new JLabel("Як справи?");
        ButtonGroup group = new ButtonGroup();
        JRadioButton aRadioButton = new JRadioButton("A");
        JRadioButton bRadioButton = new JRadioButton("Б");
        JRadioButton cRadioButton = new JRadioButton("B");
        JRadioButton dRadioButton = new JRadioButton("Г");
        JButton buttonnext = new JButton("Далі");

        panel.add(lableQuestions);
        panel.add(aRadioButton);
        group.add(aRadioButton);
        panel.add(bRadioButton);
        group.add(bRadioButton);
        panel.add(cRadioButton);
        group.add(cRadioButton);
        panel.add(dRadioButton);
        group.add(dRadioButton);

        panel2.add(buttonnext);

        ActionListener findIdAnswerActionListener = new ActionListener() {
            public void actionPerformed(ActionEvent actionEvent) {
                findIdAnswer(actionEvent);
            }
        };

        ActionListener buttonNextActionListener = new ActionListener() {
            public void actionPerformed(ActionEvent actionEvent) {
                buttonNext();
            }
        };

        aRadioButton.addActionListener(findIdAnswerActionListener);
        bRadioButton.addActionListener(findIdAnswerActionListener);
        cRadioButton.addActionListener(findIdAnswerActionListener);
        dRadioButton.addActionListener(findIdAnswerActionListener);
        buttonnext.addActionListener(buttonNextActionListener);

        panel.setBorder(BorderFactory.createCompoundBorder(BorderFactory.createEmptyBorder(4, 4, 2, 4),
                BorderFactory.createEtchedBorder(EtchedBorder.LOWERED)));
        panel2.setBorder(BorderFactory.createCompoundBorder(BorderFactory.createEmptyBorder(2, 4, 4, 4),
                BorderFactory.createEtchedBorder(EtchedBorder.LOWERED)));
        frame.add(panel, BorderLayout.CENTER);
        frame.add(panel2, BorderLayout.PAGE_END);
        frame.setSize(300, 200);
        frame.setVisible(true);
    }

    public void findIdAnswer(ActionEvent actionEvent) {
        AbstractButton aButton = (AbstractButton) actionEvent.getSource();
        System.out.println("Selected: " + aButton.getText());

        for (int i = 0; i < questions.size(); i++) {
            if (answerList.get(currentQuestions).get(i) == aButton.getText()) {
                currentAnswer = i;
            }
        }
    }

    public void buttonNext() {
        if (answersToQuestions.get(currentQuestions) == currentAnswer) {
            totalScore += 5;
        }

        takeRandomQuestions();

    }

    public void takeRandomQuestions() {
        if (complitedQuestions.size() >= 6) {
            return;
        }

        int idQuest = (int) Math.random() * 6;

        boolean isDone = false;
        for (int i = 0; i < complitedQuestions.size(); i++) {
            if (idQuest == complitedQuestions.get(i)) {
                isDone = true;
            }
        }
        int i = 0;
        while (isDone && i < 6) {
            if (++idQuest > 5) {
                idQuest = 0;
            }
            i++;
        }

        // добавити: підгрузка запитань за idQuest

    }

    public static void main(String args[]) {
        new tests();
    }
}
