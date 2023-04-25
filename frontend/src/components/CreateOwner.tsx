import React, { useState } from "react";
import "../styles/CreateOwner.css";
import { useNavigate } from "react-router-dom";
import { ICreateOwnerDto, IOwner } from "../types/global.typing";
import httpModule from "../helpers/http.module";

const CreateOwner = () => {
  const navigate = useNavigate();
  const [firstName, setFirstName] = useState("");
  const [lastName, setLastName] = useState("");
  const [firstNameError, setFirstNameError] = useState("");
  const [lastNameError, setLastNameError] = useState("");

  const createOwner = (createOwnerDto: ICreateOwnerDto) => {
    httpModule
      .post<IOwner>("/Owner/Create", createOwnerDto)
      .then((response) => {
        navigate("/");
      })
      .catch((error) => {
        console.log(error);
      });
  };

  const validate = (): boolean => {
    if (
      firstNameError.length === 0 &&
      lastNameError.length === 0 &&
      firstName.length > 0 &&
      lastName.length > 0
    ) {
      return true;
    } else {
      return false;
    }
  };

  const handleSubmit = (e: React.FormEvent<HTMLFormElement>) => {
    e.preventDefault();
    if (validate()) {
      const createOwnerDto: ICreateOwnerDto = { firstName, lastName };
      createOwner(createOwnerDto);
      e.currentTarget.reset();
    }
  };

  const handleFirstNameChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    const value = e.target.value;
    if (value.length > 30) {
      setFirstNameError("First name cannot be longer than 30 characters");
    } else {
      setFirstName(value);
      setFirstNameError("");
    }
  };

  const handleLastNameChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    const value = e.target.value;
    if (value.length > 30) {
      setLastNameError("Last name cannot be longer than 30 characters");
    } else {
      setLastName(value);
      setLastNameError("");
    }
  };

  return (
    <div className="form">
      <form onSubmit={handleSubmit}>
        <label>
          First Name:
          <input
            required
            type="text"
            name="firstName"
            value={firstName}
            onChange={handleFirstNameChange}
            placeholder="Enter owner's first name"
          />
          {firstNameError && <span className="error">{firstNameError}</span>}
        </label>
        <label>
          Last Name:
          <input
            required
            type="text"
            name="lastName"
            value={lastName}
            onChange={handleLastNameChange}
            placeholder="Enter owner's last name"
          />
          {lastNameError && <span className="error">{lastNameError}</span>}
        </label>
        <button type="submit" className="btn">
          Create Owner
        </button>
      </form>
    </div>
  );
};

export default CreateOwner;
