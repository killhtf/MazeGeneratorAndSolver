import i18n from 'i18next';
import { initReactI18next } from 'react-i18next';
import detector from 'i18next-browser-languagedetector';

const resources = {
    en: {
        translation: {
            aboutTitle: "What is this project about?",
            aboutText: "This app can generate mazes and, if you want - solve it. There are a lot of parameters for" +
                "customization (you can choice algorithms for generating and solving, adding/removing some walls; you can even " +
                "choose maze *style* - it can be colorful and bright or depressive and gray.",
            generationTitle: "Maze generation",
            generationText: "By default all mazes we create are *perfect - they have only one solution and " +
                "there are no cycles and inaccessible areas. In this project there are only \"path carving\" algorithms - " +
                "at the beginning, every cell isn't connected to each other (separated by walls). However, there are other " +
                "approaches (for example, walls generation in maze without it).",
            solveTitle: "Maze solving",
            solveText: "",
            otherTitle: "Other stuff",
            otherText: "",
        }
    },
    ru: {
        translation: {
            aboutTitle: "О чём этот проект?",
            aboutText: "Данное приложение позволяет создавать лабиринты, а затем, если есть желание - решить его. " +
                "Есть возможности для гибкой настройки параметров (выбор алгоритмов генерации и решения, " +
                "добавления или удаления стен, даже его *стиль* - можно сделать как яркий и красочный, " +
                "так и депрессивный серый лабиринт.",
            generationTitle: "Генерация лабиринта",
            generationText: "По умолчанию все лабиринты, которые мы создаём, считаются *идеальными* - в них есть только " +
                "одно решение, нет циклов и недоступных для посещения областей. На данный момент в этом проекте " +
                "используются только \"прокладывающие путь\" алгоритмы - изначально все ячейки в лабиринте не соединены " +
                "друг с другом (отделены стенами). Тем не менее, есть и другие подходы " +
                "(например, генерация стен в лабиринте, где их изначально нет.",
            rBack: "В данном алгоритме используется следующий подход:\n" +
                "0. Создаём поле нужного размера, выбираем стартовую и конечную точки. " +
                "1. В качестве стартовой точки выбирается выход (или вход) лабиринта.\n" +
                "2. Из этой точки случайным образом выбирается следующая точка, которая соединяется с предыдущей; " +
                "также эта точка добавляется в список уже посещенных." +
                "3. Так продолжается до тех пор, пока алгоритм не зайдёт в тупик - не останется свободных для посещения " +
                "точек. После этого мы удаляем точку из списка посещенных и возвращаемся к прошлой точке, после чего снова " +
                "проверяем, можно ли перейти в новую точку. Если да - см. пункт 2, иначе - удаляем ещё одну точку и проверяем " +
                "её.\n" +
                "4. Алгоритм заканчивает свою работу тогда, когда список посещенных точек окажется пустым (это будет означать, " +
                "что мы посетили все точки на поле)",
            solveTitle: "Решение лабиринта",
            solveText: "",
            otherTitle: "Прочие штуки",
            otherText: "",
        }
    }
}

i18n.use(detector).use(initReactI18next).init({
    fallbackLng: 'en',
    resources,
})

export default i18n;