import { useState } from 'react'
import reactLogo from './assets/react.svg'
import './App.css'
import { Routes, Route } from 'react-router-dom';
import Home from "./routes/home/home"

import { Navbar } from "./components";
import Login from "./routes/login/login";
import Register from "./routes/register/register";
function App() {

  return (
   
    <Routes>
        <Route  element={<Navbar />}>
          <Route index element={<Home />} />
          <Route path="login" element={<Login/>} />
          <Route path="register" element={<Register/>} />
        </Route>
      </Routes>
  )
}


export default App
