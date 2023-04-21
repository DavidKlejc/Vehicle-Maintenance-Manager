import React from "react";
import Owners from "./components/Owners";
import { atom } from "jotai";

// Global state
export const ownerIDAtom = atom("");
export const deleteOwnerByIDAtom = atom(false);

const App = () => {
  return <Owners />;
};

export default App;
