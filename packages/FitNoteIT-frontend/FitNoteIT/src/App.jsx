import { useState } from 'react'
import reactLogo from './assets/react.svg'
import './App.css'
import { Routes, Route } from 'react-router-dom';
import Home from "./routes/home/home"

import { Navbar } from "./components";
function App() {

  return (
   
    <Routes>
        <Route  element={<Navbar />}>
          <Route index element={<Home />} />
        
        </Route>
       
      </Routes>
  )
}


export default App
