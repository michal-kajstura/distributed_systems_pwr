package rsi;


import java.text.SimpleDateFormat;
import java.util.Arrays;
import java.util.Date;
import java.util.Locale;
import java.util.Vector;

public class Server {

    public static final String DATE_PATTERN = "EEEE dd MMMM yyyy hh:mm:ss";

    public Vector<String> show() {
        var methodList = new Vector<String>();
        methodList.addElement("intCalculate;Integer;Integer;String;Kalkulator;false");
        methodList.addElement("myHello;String;String;Powitanie;false");
        methodList.addElement("maxPrime;Integer;Integer;Integer;Liczba pierwsza;true");
        return methodList;
    }

    public Object callMethod(String methodName, Object[] params) {
        try {
            Class[] paramTypes = (Class[]) Arrays
               .stream(params)
               .map(Object::getClass)
               .toArray();

            var method = this.getClass()
                             .getMethod(methodName, paramTypes);
            return method.invoke(this, params);
        } catch (Exception e) {
        }
        return "Expection";
    }

    public String intCalculate(int a, int b, String c) {
        var builder = new StringBuilder();
        for (var operator: c.toCharArray()) {
            var resultStr = "";
            if (operator == '/' && b == 0) {
                resultStr = " operacja niedozwolona (dzielenie przez 0)";
            } else {
                var result = makeOperation(a, b, operator);
                resultStr = String.valueOf(result);
            }
            var equation = String.valueOf(a) + operator + String.valueOf(b) + "=" + resultStr;
            builder.append(equation + "\n");
        }
        return builder.toString();
    }

    private int makeOperation(int a, int b, char operator) {
        switch (operator) {
            case '+': return a + b;
            case '-': return a - b;
            case '*': return a * b;
            case '/': return a / b;
            case '%': return a % b;
            default: return 0;
        }
    }

    public String myHello(String name, String locale) {
        var loc = new Locale(locale);
        var dateFormat = new SimpleDateFormat(DATE_PATTERN, loc);
        var formatted = dateFormat.format(new Date());

        var builder = new StringBuilder();
        builder.append("Witaj, ");
        builder.append(name);
        builder.append('\n');
        builder.append(formatted);
        return builder.toString();
    }

    public int maxPrime(int delay, int value1, int value2) {
        try {
            Thread.sleep(delay);
        } catch (Exception e) {
        }

        int biggestPrime = 2;

        for (int i = value2; i >= value1; i--) {
            if(isPrime(i)) {
                biggestPrime = i;
                break;
            }
        }
        return biggestPrime;
    }

    private boolean isPrime(int number) {
        for (int i = 2; i <= number / 2; i++)
            if (number % i == 0)
                return false;
        return true;
    }
}
