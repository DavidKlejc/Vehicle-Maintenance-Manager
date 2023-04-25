import Owners from "./components/Owners";
import { atom } from "jotai";

// Global state
export const ownerIDAtom = atom("");

const App = () => {
  return <Owners />;
};

export default App;
