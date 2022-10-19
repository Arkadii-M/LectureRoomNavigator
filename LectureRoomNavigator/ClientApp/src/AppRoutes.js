import { Counter } from "./components/Counter";
import { FetchData } from "./components/FetchData";
import { Home } from "./components/Home";
import { Test } from "./components/Test";
import { MapExample } from "./components/MapExample";
const AppRoutes = [
    {
        index: true,
        element: <Home />
    },
    {
        path: '/counter',
        element: <Counter />
    },
    {
        path: '/fetch-data',
        element: <FetchData />
    },
    {
        path: '/test-data',
        element: <Test />
    },
    {
        path: '/map-example',
        element: <MapExample />
    }

    
];

export default AppRoutes;
