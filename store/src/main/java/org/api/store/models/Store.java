package org.api.store.models;

import java.util.UUID;

import javax.persistence.Column;
import javax.persistence.Entity;
import javax.persistence.GeneratedValue;
import javax.persistence.GenerationType;
import javax.persistence.Id;
import javax.persistence.SequenceGenerator;
import javax.persistence.Table;

import lombok.AllArgsConstructor;
import lombok.Builder;
import lombok.Data;
import lombok.NoArgsConstructor;

@Data
@Builder
@AllArgsConstructor
@NoArgsConstructor
@Entity
@Table(name="Store")
public class Store {
	
	@Id
	@Column(name = "Id")
	@SequenceGenerator(
			name = "store_id_sequence",
			sequenceName = "store_id_sequence")
	@GeneratedValue(
			strategy = GenerationType.SEQUENCE,
			generator = "store_id_sequence")
	private Integer id;
	@Column(name = "Store_Code")
	private UUID storeCode;
	@Column(name = "Address")
	private String address;
	
}
